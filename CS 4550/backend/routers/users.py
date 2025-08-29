from fastapi import APIRouter, Depends

from typing import Literal

from backend import database as db

from sqlmodel import Session


users_router = APIRouter(prefix="/users", tags=["Users"])

from backend.entities import (
    User,
    UserCollection,
    UserCreate,
    UserFound,
    UserChats,
    Chat,
    UserUpdate,
)

from backend.schema import(
    UserInDB
)

from backend.auth import *

@users_router.get("", response_model=UserCollection, description="This allows you to get all users from the database")
def get_all_users(session: Session = Depends(db.get_session)):
    sort_key = lambda user: getattr(user, "id")
    users = db.get_all_users(session)
    return UserCollection(
        meta={"count": len(users)},
        users=sorted(users, key=sort_key),
        
    )


@users_router.get("/me", description="This allows you to return the currently logged in user.")
def get_user_me(user: UserInDB = Depends(get_current_user)):
    return UserFound(
        user=user
    )
    
@users_router.put("/me", description="This allows you to change the curren user's username or email.")
def put_user_me(user_update: UserUpdate, session: Session = Depends(db.get_session), user: UserInDB = Depends(get_current_user)):
    user = db.put_user_me(user_update, session, user)
    return UserFound(
        user=user
    )

@users_router.get("/{user_id}", description="This allows you to return a user for a given ID. 404 error if not found.")
def  get_user_by_id(user_id: int, session: Session = Depends(db.get_session)):
    user = db.get_user_by_id(session, user_id)
    return UserFound(
        user=user)

@users_router.get("/{user_id}/chats", description="This allows you to return what chats a user has been in. Errors 404 if not found.")
def get_user_chat(user_id: int, session: Session = Depends(db.get_session)):
    sort_key = lambda chat: getattr(chat, "name")
    list = db.get_user_chat(session, user_id)
    return UserChats(
        meta={"count": len(list)},
        chats=sorted(list, key=sort_key),
    )
    
