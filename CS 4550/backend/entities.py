from datetime import date, datetime
from typing import Optional

from pydantic import BaseModel

from sqlmodel import Field, Relationship, SQLModel








#   -------- users --------   #
class User(SQLModel, table=True):
    """Represents an API response for a user."""
    
    id: int = Field(default=None, primary_key=True)
    username: str = Field(unique=True, index=True)
    email: str = Field(unique=True)
    created_at: Optional[datetime] = Field(default_factory=datetime.now)

#   -------- user request models--------   #

class UserFound(BaseModel):
    user: User

class UserUpdate(BaseModel):
    username: Optional[str] = Field(default=None)
    email: Optional[str] = Field(default=None)


class UserCreate(SQLModel):
    """Represents parameters for adding a new user to the system."""
    id: int
    username: str
    email: str
    hashed_password: str
    created_at: Optional[datetime] = Field(default_factory=datetime.now)



class Metadata(SQLModel):
    """Represents metadata for a collection."""
    count: int

class UserCollection(SQLModel):
    """Represents an API response for a collection of users."""

    meta: Metadata
    users: list[User]

# -------- messages --------  #
class Message(BaseModel):

    
    id: int = Field(default=None)
    text: str
    chat_id: int
    user: User
    created_at: Optional[datetime] = Field(default_factory=datetime.now)

    
class AddMessage(BaseModel):
    text: str
    
class ResponseMessage(BaseModel):
    message: Message


 #   -------- chat --------   #
class Chat(SQLModel):
    """Represents an API response for a chat."""
    
    id: int = Field(default=None, primary_key=True)
    name: str
    owner: User
    created_at: datetime
    
    
class ChatWithMSG(SQLModel):
    """Chat info with messages."""
    id: int = Field(default=None, primary_key=True)
    name: str
    user_ids: list[int] = Field(default=None, foreign_key="users.id")
    owner_id: str
    created_at: datetime
    messages: list[Message] = Field(default=None, foreign_key="messages")

class ChatCollection(SQLModel):
    meta: Metadata
    chats: list[Chat]
    
class ChatMeta(BaseModel):
    message_count: int
    user_count: int
    
class ChatFound(BaseModel):
    meta: Optional[ChatMeta] = Field(default=None) 
    chat: Chat
    messages: Optional[list[Message]] = Field(default=None) 
    users: Optional[list[User]] = Field(default=None) 
   
    

class ChatUpdate(SQLModel):
    name: str = None
    
class ChatMessages(SQLModel):
    meta: Metadata
    messages: list[Message]
    
class UserChats(BaseModel):
    meta: Metadata
    chats: list[Chat]
    
