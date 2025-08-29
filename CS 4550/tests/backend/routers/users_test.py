from fastapi.testclient import TestClient

from backend.main import app

def test_get_all_users():
    client = TestClient(app)
    response = client.get("/users")
    assert response.status_code == 200
    
def test_create_user():
    client = TestClient(app)
    response = client.post("/users", json={"id": "funny"},)
    assert response.status_code == 200
    
def test_get_user_by_id():
    client = TestClient(app)
    response = client.get("/users/bishop")
    assert response.status_code == 200
    
def test_get_user_by_id_fail():
    client = TestClient(app)
    response = client.get("/users/sdfuajndv")
    assert response.status_code == 404
    
def test_get_user_chats():
    client = TestClient(app)
    response = client.get("/users/bishop/chats")
    assert response.status_code == 200
