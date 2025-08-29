from fastapi.testclient import TestClient

from backend.main import app

def test_get_all_chats():
    client = TestClient(app)
    response = client.get("/chats")
    assert response.status_code == 200
    
def test_get_chat_by_id():
    client = TestClient(app)
    response = client.get("/chats/6215e6864e884132baa01f7f972400e2",)
    assert response.status_code == 200
    
def test_change_name():
    client = TestClient(app)
    response = client.put("/chats/6215e6864e884132baa01f7f972400e2", json={"name": "lol"})
    assert response.status_code == 200
    
def test_chat_deletionl():
    client = TestClient(app)
    response = client.delete("/chats/6215e6864e884132baa01f7f972400e2")
    assert response.status_code == 204
    
def test_get_user_chats():
    client = TestClient(app)
    response = client.get("/chats/660c7a6bc1324e4488cafabc59529c93/messages")
    assert response.status_code == 200

def test_get_message_log():
    client = TestClient(app)
    response = client.get("/chats/660c7a6bc1324e4488cafabc59529c93/users")
    assert response.status_code == 200
