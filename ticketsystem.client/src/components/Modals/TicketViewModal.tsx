import { useEffect, useState } from "react";
import { toast } from "sonner";
import {Modal, Button} from "react-bootstrap"
import Dropdown from 'react-dropdown'

interface Ticket {
    id: number;
    submittedby: string;
    title: string;
    description: string;
    priorityid: number;
    categoryid: number;
    stageid: number;
    affectedusers: string[];
}

interface Timelog {
    id: number;
    assignedTo: string;
    totalHoursSpent: number;
    description: string;
    creationDate: string;
}

interface Assigne {
    id: number;
    username: string;
}

interface Supporter {
    id: number;
    username: string;
}


function TicketViewModal({ ticket, show, onHide }: { ticket: Ticket; show: boolean; onHide: () => void }) {
    const [title, setTitle] = useState<string>(ticket.title);
    const [description, setDescription] = useState<string>(ticket.description);
    const [priority, setPriority] = useState<number>(ticket.priorityid);
    const [category, setCategory] = useState<number>(ticket.categoryid);
    const [stage, setStage] = useState<number>(ticket.stageid);
    const [username, setUsername] = useState<string>("");
    const [userResponse, setUserResponse] = useState<string>("");
    const [selectedUsernames, setSelectedUsernames] = useState<string[]>(ticket.affectedusers);
    const [assignes, setAssignes] = useState<Assigne[]>([]);
    const [supporters, setSupporters] = useState<Supporter[]>([]);
    const [timelogs, setTimelogs] = useState<Timelog[]>([]);
    const [showLogTime, setShowLogTime] = useState<boolean>(false);
    const [showAssignUsers, setShowAssignUsers] = useState<boolean>(false);
    
     const handleShowLogTime = () => {
        if (showLogTime == true){
            setShowLogTime(false);
        }else{
            setShowAssignUsers(false)
            setShowLogTime(true);
        }
     } 

     const handleShowAssignUsers = () => {
        if (showAssignUsers == true){
            setShowAssignUsers(false);
        }else{
            setShowLogTime(false);
            setShowAssignUsers(true);
        }
     } 

    async function handleTicketSubmit (event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget
        const formData = new FormData(form)
        const data = {
            ...Object.fromEntries(formData.entries()),
            Usernames: selectedUsernames
        };
        
        const response = await fetch('/ticket/UpdateTicket', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });
        if (response.ok) {
            toast.success("Ticket created successfully");
    }};

    async function handleTimelogSubmit (event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget
        const formData = new FormData(form)
        const data = {
            ...Object.fromEntries(formData.entries()),
            TicketId: ticket.id
        };
        
        const response = await fetch('/timelog/AddTimelog', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });
        if (response.ok) {
            toast.success("Time logged successfully");
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

    async function handleAssignUser(username: string) {
        const response = await fetch('/assignedticket/AssignTicket', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                ticketId: ticket.id,
                username: username
            })
        });

        if (response.ok) {
            toast.success("Assigned user to ticket");
        }
    }

    const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setUsername(e.target.value);
    };
    
    const removeUsername = (usernameToRemove: string) => {
        setSelectedUsernames((current) => current.filter((name) => name !== usernameToRemove));
    };

    async function GetAssignes() {
        const response = await fetch(`/assignedticket/GetAssignesByTicketId/${encodeURIComponent(ticket.id)}`);
        if (response.ok) {
            const data = await response.json();
            setAssignes(data);
        }};

    async function GetAllSupports() {
        const response = await fetch(`/user/GetAllSupportUsers`);
        if (response.ok) {
            const data = await response.json();
            setSupporters(data);
        }};
    
    async function GetAllTimelogs() {
        const response = await fetch(`/timelog/GetTimelogsByTicketId/${encodeURIComponent(ticket.id)}`);
        if (response.ok) {
            const data = await response.json();
            setTimelogs(data);
        }};
    
      useEffect(() => {
        GetAssignes();
        GetAllSupports();
        GetAllTimelogs();
      }, []);


    async function GetUser() {
        if (username.trim())
        {
            const response = await fetch(`/user/GetUserByUsername/${encodeURIComponent(username)}`);
            if (response.ok) {
                const data = await response.text();
                setUserResponse(data);
            }
        } 
        else {
            setUserResponse("");
        }
    };   

    useEffect(() => {
        GetUser();
    }, [username]);


        const options = supporters === undefined ? [] : supporters.map((support) => ({
        value: support.id,
        label: support.username,
        }));

    return (
        <div
        className="modal show"
        style={{ display: 'block', position: 'initial' }}
        >
    <Modal show={show} onHide={onHide}>
        <Modal.Header closeButton>
          <Modal.Title>Ticket {ticket.id}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <form className="" id="ticketForm" onSubmit={handleTicketSubmit}>
                <div>
                    <label>Title</label>
                    <input className="form-control" 
                        name="Title" 
                        placeholder="Enter ticket title" 
                        disabled
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        ></input>
                </div>
                <div>
                    <label>Description</label>
                    <textarea
                        name="Description" 
                        className="form-control" id="exampleFormControlTextarea1" 
                        rows={5} onChange={(e) => setDescription(e.target.value)} 
                        placeholder="Enter ticket description" 
                        disabled
                        value={description}>
                        </textarea>
                </div>
                <div>
                    <label>Priority</label>
                   <input className="form-control" 
                        name="PriorityId" 
                        placeholder="Enter ticket priority" 
                        required
                        value={priority}
                        type="number"
                        min="1"
                        max="3"
                        onChange={(e) => setPriority(e.target.valueAsNumber)}
                        ></input>
                </div>
                <div>
                    <label>Category</label>
                    <input className="form-control" 
                        name="CategoryId" 
                        placeholder="Enter ticket category" 
                        required
                        value={category}
                        type="number"
                        min="1"
                        max="3"
                        onChange={(e) => setCategory(e.target.valueAsNumber)}
                        ></input>
                </div>
                <div>
                    <label>Stage</label>
                    <input className="form-control" 
                        name="StageId" 
                        placeholder="Enter ticket stage" 
                        required
                        value={stage}
                        type="number"
                        min="1"
                        max="3"
                        onChange={(e) => setStage(e.target.valueAsNumber)}
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
                        className="form-control w-100"
                        placeholder="Search for affected user"
                        value={username}
                        onChange={handleUsernameChange}
                    />
                    {username.length > 0 && userResponse.length > 0 && !selectedUsernames.includes(userResponse) &&
                        <button onClick={handleChooseUsername} type="button">{userResponse}</button>}
                </div>
        </form>
                <div className="d-flex gap-2 mt-3">
                    <button className="btn btn-info" onClick={handleShowLogTime}>Log time</button>
                    <button className="btn btn-info"  onClick={handleShowAssignUsers}>Assign users</button>
                </div>                             
                {
                    showLogTime ? (
                    <>
                        <form className="w-50 mx-auto d-flex flex-column form-group gap-2" onSubmit={handleTimelogSubmit}>
                            <label>Create timelog</label>
                            <input className="form-control" name="TimeSpent" placeholder="Enter timelog time" type="number" required></input>
                            <input className="form-control" name="description" placeholder="Enter description" required></input>
                            <button className="w-50 align-self-center btn btn-primary" type="submit">Submit</button>
                        </form>
                        <h2>Time log</h2>
                        <table className="table table-light table-striped" aria-labelledby="tableLabel">
                            <thead>
                                <tr>
                                    <th>Logged by</th>
                                    <th>Hours logged</th>
                                    <th>Description</th>
                                    <th>logged date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {timelogs.map(timelog =>
                                    <tr key={timelog.id}>
                                        <td>{timelog.assignedTo}</td>
                                        <td>{timelog.totalHoursSpent}</td>
                                        <td>{timelog.description}</td>
                                        <td>{timelog.creationDate}</td>
                                    </tr>
                                )}
                            </tbody>    
                        </table>
                        </>
                        ) : showAssignUsers ? (
                        <>
                        <h2>Assigned users</h2>
                        <Dropdown
                            controlClassName="btn btn-outline-primary dropdown-toggle"
                            menuClassName="list-group"
                            optionClassName="list-group-item list-group-item-action"
                            aria-label="Assigned"
                            options={options}
                            onChange={(option) => {
                            const support = supporters?.find(
                                (support) => support.id === option.value
                            );

                            if (support) {
                                handleAssignUser(support.username);
                            }
                            }}      
                            placeholder="Select a person to assign"
                        />
                        <table className="mt-4 w-50 mx-auto table table-light table-striped" aria-labelledby="tableLabel">
                            <thead>
                                <tr>
                                    <th>Username</th>
                                </tr>
                            </thead>
                            <tbody>
                                {supporters.map(support =>
                                    <tr key={support.id}>
                                        <td>{support.username}</td>
                                    </tr>
                                )}
                            </tbody>    
                        </table>
                        </>
                        ) : <div></div>
                }

                        
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={onHide}>Close</Button>
          <button className="mt-1 btn btn-success" form="ticketForm" type="submit">Save changes</button>
        </Modal.Footer>
      </Modal>
    </div>
    )

}

export default TicketViewModal;