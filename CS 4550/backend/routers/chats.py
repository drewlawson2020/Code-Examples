from fastapi import APIRouter, Depends, Query; 

from typing import Literal;

from backend import database as db;

from sqlmodel import Session;


chats_router = APIRouter(prefix="/chats", tags=["Chats"])

from backend.entities import (
    Chat,
    ChatCollection,
    ChatFound,
    ChatUpdate,
    Message,
    ChatMessages,
    UserCollection,
    AddMessage,
    ResponseMessage,
    ChatMeta
)

from backend.schema import(
    UserInDB,
    MessageInDB,
    
)

from backend.auth import *

@chats_router.get("", response_model=ChatCollection, description="This allows you to return what chats a user has been in. Errors 404 if not found.")
def get_all_chats(session: Session = Depends(db.get_session)):
    sort_key = lambda chat: getattr(chat, "name")
    chats = db.get_all_chats(session)
    return ChatCollection(
        meta={"count": len(chats)},
        chats=sorted(chats, key=sort_key),
    )

@chats_router.get("/{chat_id}", response_model=ChatFound, response_model_exclude_none=True, description="This allows you to return a chat with a given ID. Errors 404 if not found.")
def get_chat_by_id(chat_id: int, include: Annotated[list[str], Query()] = [], session: Session = Depends(db.get_session)):
    chat = db.get_chat_by_id(session, chat_id)
    messages = None
    users = None
    if "messages" in include:
        messages= list()
        for m in chat.messages:
            messages.append(Message(
                id= m.id,
                text= m.text,
                chat_id= m.chat_id,
                user = m.user,
                created_at= m.created_at,
                
            ))
            
    if "users" in include:
        users = chat.users
    meta = ChatMeta(
        message_count = len(chat.messages),
        user_count = len(chat.users),
    )
    return ChatFound(
        meta= meta,
        chat= chat,
        messages= messages,
        users= users,
        )
    
@chats_router.put("/{chat_id}", response_model=ChatFound, response_model_exclude_none=True, description="This allows you to update a chat with a given ID. Errors 404 if not found.")
def put_update_chat_name(chat_id: int, chat_update: ChatUpdate, session: Session = Depends(db.get_session)):
    chat = db.put_update_chat_name(session, chat_id, chat_update)
    return ChatFound(
        chat=chat
        )
    
    
@chats_router.get("/{chat_id}/messages", description="This allows you to get a chat list with a given ID. Errors 404 if not found.")
def get_chat_messages(chat_id: int, session: Session = Depends(db.get_session)) -> ChatMessages:
    messages = db.get_chat_messages(session, chat_id)
    messagelist= list()
    for m in messages:
        messagelist.append(Message(
            id= m.id,
            text= m.text,
            chat_id= m.chat_id,
            user = m.user,
            created_at= m.created_at,
                
            ))
    return ChatMessages(
    meta={"count": len(messagelist)},
    messages= messagelist
    )

@chats_router.get("/{chat_id}/users", description="This allows you to get a user list with a given ID if found in a chat. Errors 404 if not found.")
def get_users_from_chat(chat_id: int, session: Session = Depends(db.get_session)) -> UserCollection:
    users = db.get_users_from_chat(session, chat_id)
    return UserCollection(
    meta={"count": len(users)},
    users = users
    )
    
@chats_router.post("/{chat_id}/messages", status_code=201, description="This allows you to send a message to a specified chat.")
def post_message_to_chat(addmessage: AddMessage, chat_id: int, session: Session = Depends(db.get_session), user: UserInDB = Depends(get_current_user)) -> ResponseMessage:
    message = db.post_message_to_chat(addmessage, session, chat_id, user)
    messages = db.get_chat_messages(session, chat_id)
    toReturn = Message(
        id=messages[-1].id + 1,
        text= message.text,
        chat_id= chat_id,
        user = user,   
    )
    return ResponseMessage(
        message= toReturn
    )

