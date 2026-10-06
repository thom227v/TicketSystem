import { useEffect, useState } from "react";
import { toast } from "sonner";

function Create() {
    const [username, setUsername] = useState<string>();
    const [userReponse, setUserResponse] = useState<string>();

    async function handleTicketSubmit (event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget
        const formData = new FormData(form)
        formData.append("Username", JSON.stringify(username));
        const data = Object.fromEntries(formData.entries());   
        
        const response = await fetch('/ticket/CreateTicket', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });
        if (response.ok) {
            toast.success("Ticket created successfully");
    }};

    const handleChoosenUsername = () => {
        setUsername(userReponse);
        toast.success("Valgt affected user er ændret");
    }

    const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setUsername(e.target.value);
    };
        
    useEffect(() => {
    async function GetUser() {
        if (username !== undefined)
        {
            const response = await fetch(`/user/GetUserByUsername/${username}`);
            if (response.ok) {
                const data = await response.text();
                setUserResponse(data);
            }};
        }   
        GetUser();
    }, [username]);


    const usernamePartial = username === undefined 
        ? <input name="AffectedUsername" placeholder="Enter username of affected user" onChange={handleUsernameChange}></input>
        :   <div>
                <input name="AffectedUsername" placeholder="Enter username of affected user" onChange={handleUsernameChange}></input>
                <button onClick={handleChoosenUsername} type="button">{userReponse}</button>
            </div>

    return (
        <>
            <form className="w-50 mx-auto d-flex flex-column form-group gap-2" onSubmit={handleTicketSubmit}>
                <label>Fill out ticket blanket</label>
                <input className="form-control" name="Title" placeholder="Enter ticket title" required></input>
                <input className="form-control" name="Description" placeholder="Enter ticket description" required></input>
                <input className="form-control" name="Priority" placeholder="Enter ticket priority" required></input>
                <input className="form-control" name="Category" placeholder="Enter ticket category" required></input>
                <input name="AffectedUsername" placeholder="Enter username of affected user" onChange={handleUsernameChange}></input>
                {username?.length && userReponse?.length
                ? <button onClick={handleChoosenUsername} type="button">{userReponse}</button> 
                : <div hidden></div>}
                <button className="mt-1 btn btn-primary" type="submit">Create Ticket</button>
            </form>
        </>
    );
}

export default Create;