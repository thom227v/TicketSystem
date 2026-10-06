import { useEffect, useState } from "react";
import { toast } from "sonner";

function Create() {
    const [username, setUsername] = useState<string>("");
    const [userResponse, setUserResponse] = useState<string>("");
    const [selectedUsernames, setSelectedUsernames] = useState<string[]>([]);

    async function handleTicketSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget;
        const formData = new FormData(form);
        const data = {
            ...Object.fromEntries(formData.entries()),
            Username: selectedUsernames[0] ?? "",
            Usernames: selectedUsernames
        };

        const response = await fetch('/ticket/CreateTicket', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });
        if (response.ok) {
            toast.success("Ticket created successfully");
        }
    }

    const handleChooseUsername = () => {
        if (!userResponse || selectedUsernames.includes(userResponse)) {
            return;
        }

        setSelectedUsernames((current) => [...current, userResponse]);
        setUsername("");
        setUserResponse("");
        toast.success("Affected user added");
    };

    const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setUsername(event.target.value);
    };

    const removeUsername = (usernameToRemove: string) => {
        setSelectedUsernames((current) => current.filter((name) => name !== usernameToRemove));
    };

    useEffect(() => {
        async function getUser() {
            if (!username.trim()) {
                setUserResponse("");
                return;
            }

            const response = await fetch(`/user/GetUserByUsername/${encodeURIComponent(username)}`);
            if (response.ok) {
                setUserResponse(await response.text());
            }
        }

        getUser();
    }, [username]);

    return (
        <form className="w-50 mx-auto d-flex flex-column form-group gap-2" onSubmit={handleTicketSubmit}>
            <label>Fill out ticket blanket</label>
            <input className="form-control" name="Title" placeholder="Enter ticket title" required />
            <input className="form-control" name="Description" placeholder="Enter ticket description" required />
            <input className="form-control" name="Priority" placeholder="Enter ticket priority" required />
            <input className="form-control" name="Category" placeholder="Enter ticket category" required />

            <label htmlFor="affected-username">Affected users</label>
            <div className="d-flex flex-wrap gap-2">
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
                id="affected-username"
                placeholder="Search for affected user"
                value={username}
                onChange={handleUsernameChange}
            />
            {username.length > 0 && userResponse.length > 0 && !selectedUsernames.includes(userResponse) && (
                <button onClick={handleChooseUsername} type="button">{userResponse}</button>
            )}
            <button className="mt-1 btn btn-primary" type="submit">Create Ticket</button>
        </form>
    );
}

export default Create;