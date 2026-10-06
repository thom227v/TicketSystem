import { useEffect, useState } from "react";
import { toast } from "sonner";
import {Modal, Button} from "react-bootstrap"

interface Ticket {
    id: number;
    submittedby: string;
    title: string;
    description: string;
    priority: number;
    category: number;
    affectedusers: string[];
}

function TicketViewModal({ ticket, show, onHide }: { ticket: Ticket; show: boolean; onHide: () => void }) {
    const [title, setTitle] = useState<string>(ticket.title);
    const [description, setDescription] = useState<string>(ticket.description);
    const [priority, setPriority] = useState<number>(ticket.priority);
    const [category, setCategory] = useState<number>(ticket.category);
    const [username, setUsername] = useState<string>("");
    const [userResponse, setUserResponse] = useState<string>("");
    const [selectedUsernames, setSelectedUsernames] = useState<string[]>(ticket.affectedusers);
    
    async function handleTicketSubmit (event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget
        const formData = new FormData(form)
        const data = {
            ...Object.fromEntries(formData.entries()),
            Usernames: selectedUsernames
        };
        
        console.log(selectedUsernames)
        console.log(event.currentTarget)

        const response = await fetch('/ticket/UpdateTicket', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });
        if (response.ok) {
            toast.success("Ticket created successfully");
    }};

    
    const handleChooseUsername = () => {
        if (!userResponse || selectedUsernames.includes(userResponse)) {
            return;
        }

        setSelectedUsernames((current) => [...current, userResponse]);
        setUsername("");
        setUserResponse("");
        toast.success("Affected user added");
    };

    const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setUsername(e.target.value);
    };

    const removeUsername = (usernameToRemove: string) => {
        setSelectedUsernames((current) => current.filter((name) => name !== usernameToRemove));
    };

    useEffect(() => {
    async function GetUser() {
        if (username.trim())
        {
            const response = await fetch(`/user/GetUserByUsername/${encodeURIComponent(username)}`);
            if (response.ok) {
                const data = await response.text();
                setUserResponse(data);
            }
        } else {
            setUserResponse("");
        }
        }   
        GetUser();
    }, [username]);


    return (
        <div
        className="modal show"
        style={{ display: 'block', position: 'initial' }}
        >
    <Modal show={show} onHide={onHide}>
        <Modal.Header closeButton>
          <Modal.Title>View Ticket {ticket.id}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <form className="w-50 mx-auto d-flex flex-column form-group gap-2" onSubmit={handleTicketSubmit}>
                <h2>Edit Ticket</h2>
                <div>
                    <label>Title</label>
                    <input className="form-control" 
                        name="Title" 
                        placeholder="Enter ticket title" 
                        required
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        ></input>
                </div>
                <div>
                    <label>Description</label>
                    <input className="form-control" 
                        name="Description" 
                        placeholder="Enter ticket description" 
                        required
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        ></input>
                </div>
                <div>
                    <label>Priority</label>
                   <input className="form-control" 
                        name="Priority" 
                        placeholder="Enter ticket priority" 
                        required
                        value={priority}
                        type="number"
                        min="1"
                        max="5"
                        onChange={(e) => setPriority(e.target.valueAsNumber)}
                        ></input>
                </div>
                <div>
                    <label>Category</label>
                    <input className="form-control" 
                        name="Category" 
                        placeholder="Enter ticket category" 
                        required
                        value={category}
                        type="number"
                        min="1"
                        max="5"
                        onChange={(e) => setCategory(e.target.valueAsNumber)}
                        ></input>
                </div>
                <div>
                    <label>Affected party</label>
                    <div className="d-flex flex-wrap gap-2 mb-2">
                        {selectedUsernames.map((selectedUsername) => (
                            <span className="badge text-bg-secondary d-inline-flex align-items-center gap-1" key={selectedUsername}>
                                {selectedUsername}
                                <button
                                    type="button"
                                    className="btn-close btn-close-white"
                                    aria-label={`Remove ${selectedUsername}`}
                                    onClick={() => removeUsername(selectedUsername)}
                                />
                            </span>
                        ))}
                    </div>
                    <input
                        placeholder="Search for affected user"
                        value={username}
                        onChange={handleUsernameChange}
                    />
                    {username.length > 0 && userResponse.length > 0 && !selectedUsernames.includes(userResponse) &&
                        <button onClick={handleChooseUsername} type="button">{userResponse}</button>}
                </div>
                <button className="mt-1 btn btn-primary" type="submit">Update ticket</button>
           </form>
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={onHide}>Close</Button>
        </Modal.Footer>
      </Modal>
    </div>
    )

}

export default TicketViewModal;