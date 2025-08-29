import json
from datetime import date
from datetime import datetime
from sqlmodel import Session, SQLModel, create_engine, select



engine = create_engine(
    "sqlite:///backend/pony_express.db",
    echo=True,
    connect_args={"check_same_thread": False},
)


def create_db_and_tables():
    SQLModel.metadata.create_all(engine)


def get_session():
    with Session(engine) as session:
        yield session

from backend.entities import (
    User,
    UserCreate,
    UserFound,
    Chat,
    ChatCollection,
    ChatUpdate,
    ChatFound,
    Message,
    ChatMessages,
    UserCollection,
    ChatWithMSG,
    Chat,
    UserUpdate,
)

from backend.schema import(
    UserChatLinkInDB,
    UserInDB,
    ChatInDB,
    MessageInDB,
)




class EntityNotFoundException(Exception):
    def __init__(self, *, entity_name: str, entity_field: str, entity_value: str):
        self.entity_name = entity_name
        self.entity_field = entity_field
        self.entity_value = entity_value

        
class EntityDuplicateException(Exception):
    def __init__(self, *, entity_name: str, entity_field: str, entity_value: str):
        self.entity_name = entity_name
        self.entity_field = entity_field
        self.entity_value = entity_value


#   -------- users --------   #

def get_all_users(session: Session) -> list[User]:
    """
    Retrieve all users from the database.

    :return: ordered list of users
    """
    return session.exec(select(UserInDB)).all()


def create_user(session: Session, user_create: UserCreate) -> User:
    """
    Create a new user in the database.

    :param user_create: attributes of the user to be created
    :return: the newly created user
    """
    user = UserInDB(**user_create.model_dump())
    session.add(user)
    session.commit()
    session.refresh(user)
    return user        


def get_user_by_id(session: Session, user_id: int) -> User:
    """
    Retrieve an user from the database.

    :param user_id: id of the user to be retrieved
    :return: the retrieved user
    """
    user = session.get(UserInDB, user_id)
    if user:
        return user

    raise EntityNotFoundException(entity_name="User", entity_id=user_id)
        
def get_user_chat(session: Session, user_id: int):
        User = get_user_by_id(session, user_id)
        return User.chats
        
                


        

# def get_chat_by_user(user_id: str) -> list[Chat]: # 

#   -------- chats --------   #

def get_all_chats(session: Session) -> list[Chat]:
    """
    Retrieve all chats from the database.

    :return: ordered list of chats
    """
    return session.exec(select(ChatInDB)).all()

def get_chat_by_id(session: Session, chat_id: int) -> Chat:
    """
    Retrieve all chats from the database.

    :return: ordered list of chats
    """
    chat = session.get(ChatInDB, chat_id)
    if chat:
        return chat

    raise EntityNotFoundException(entity_name="Chat", entity_id=chat_id)
    
def put_update_chat_name(session: Session, chat_id: int, chat_update: ChatUpdate) -> ChatFound:
    """
    Update the chat's name by finding its id

    :return: the modified chat
    """
    chat = get_chat_by_id(session, chat_id)
    chat.name = chat_update.name
    session.add(chat)
    session.commit()
    session.refresh(chat)
    return chat
        

def get_chat_messages(session: Session, chat_id: int) -> ChatMessages:
    """
    Get chat messages by ID
    
    :return: the chat messages
    """
    chat = session.get(ChatInDB, chat_id)
    if chat.messages:
        return chat.messages
    
        
def get_users_from_chat(session: Session, chat_id: int) -> list[User]:
    """
    Get users by chat ID
    
    :return: a list of users
    """
    try:
        chat = session.get(ChatInDB, chat_id)
        return chat.users

    except:
        raise EntityNotFoundException(
            entity_name = "Chat",
            entity_id = chat_id,
        )
def get_user_me(user: UserInDB):
    if user:
        return user
    else:
        raise EntityNotFoundException(
            entity_name = "User"
        )
        
def put_user_me(user_update: UserUpdate, session: Session, user: UserInDB):
    if user:
        if user_update.email is not None:
            user.email = user_update.email
        if user_update.username is not None:
            user.username = user_update.username


        return user
            
            
    else:
        raise EntityNotFoundException(
            entity_name = "User"
        )
        
def post_message_to_chat(addmessage, session, chat_id, user):
    if user:
        chat = get_chat_by_id(session, chat_id)
        message = MessageInDB(
            chat_id=chat.id,
            user_id=user.id,
            text=addmessage.text,
        )
        session.add(message)
        session.commit()
        session.refresh(message)
        return message
        
    
