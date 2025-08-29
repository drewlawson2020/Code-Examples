import { useState } from "react";
import { useQuery } from "react-query";
import { Link, useParams } from "react-router-dom";
import "./Chats.css";
import Button from "./Button";
import FormInput from "./FormInput";
import ScrollContainer from "./ScrollContainer";
import LeftNav from "./LeftNav";
import { useMutation, useQueryClient } from "react-query";
import { useAuth, useApi } from "../hooks";
// ---------------------CHATS---------------------




function Input(props) {
  return (
    <div className="flex flex-col py-2">
      <label className="text-s text-gray-400" htmlFor={props.name}>
        {props.name}
      </label>
      <input
        {...props}
        className="border rounded bg-transparent px-2 py-1"
      />
    </div>
  );
}

function NewMessage({ chatId }) {
  const queryClient = useQueryClient();
  const { token } = useAuth();
  const api = useApi();

  const [text, setMessage] = useState("");

  const mutation = useMutation({
    mutationFn: () => (
      api.post(
        "/chats/" + chatId + "/messages",
        {
          text
        },
      ).then((response) => response.json())
    ),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["messages", chatId],
      });
    },
  });

  const onSubmit = (e) => {
    e.preventDefault();
    mutation.mutate();
  };

  return (
    <form onSubmit={onSubmit}>
      <Input
        name="message"
        type="text"
        value={text}
        onChange={(e) => setMessage(e.target.value)}
      />
      <Button type="submit">submit</Button>
    </form>
  );
}

function MessageCard({ messages }) {
  return (
    <ScrollContainer>
      {messages.map((message) => (
        <div key={message.id} className="message-card">
          <div className="message-card-meta">
            <div className="message-card-attr">
              {message.user.username}
              &nbsp;
              { (new Date(message.created_at)).toLocaleDateString()}
              &nbsp;
              { (new Date(message.created_at)).toLocaleTimeString()}
            </div>
          </div>
          <div key={message.id} className="message-card-text">
            {message.text}
          </div>
        </div>
      ))}
    </ScrollContainer>
  )
}


function MessageCardContainer({ messages }) {
  return (
    <div className="flex flex-col border-b-2 border-green-400 h-messages">
      <MessageCard messages={messages} />
      <NewMessage chatId={messages[0].chat_id} />
      </div>

  );
}


function MessageListContainer() {
  const { chatId } = useParams();
  const { data } = useQuery({
    queryKey: ["messages", chatId],
    queryFn: () => (
      fetch(`http://127.0.0.1:8000/chats/${chatId}/messages`)
        .then((response) => response.json())
    ),
  });

  if (data?.chats) {
    return (
      <div className="message-list-container">
        <h2>messages</h2>
        <MessageList chats={data.chats} />
      </div>
    )
  }

  return
}

// Called first, and uses a query to gather all chats by ID
function MessageCardQueryContainer({ chatId }) {
  const { data } = useQuery({
    queryKey: ["messages", chatId],
    queryFn: () => (
      fetch(`http://127.0.0.1:8000/chats/${chatId}/messages`)
        .then((response) => response.json())
    ),
  });
  
  if (data && data.messages) {
    return <MessageCardContainer messages={data.messages} />
  }

  return <h2>loading...</h2>
}



function Chats() {
  const { chatId } = useParams();
  return (
  <div className="flex flex-row">
    <LeftNav></LeftNav>
      <div className="flex flex-col border-r-2 border-green-400">
        {chatId ? <MessageCardQueryContainer chatId={chatId} /> : <h2>Messages</h2>}</div>
        </div> 
  );
}



export default Chats;