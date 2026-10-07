import { useEffect, useState } from "react";
import { toast } from "sonner";
import Dropdown from 'react-dropdown'

interface Priority {
    id: number;
    prioritylabel: string;
}

interface Category {
    id: number;
    categorylabel: string;
}


function Create() {
    const [username, setUsername] = useState<string>("");
    const [userResponse, setUserResponse] = useState<string>("");
    const [selectedUsernames, setSelectedUsernames] = useState<string[]>([]);
    const [priorities, setPriorities] = useState<Priority[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    const [priority, setPriority] = useState<Priority>();
    const [category, setCategory] = useState<Category>();

    async function handleTicketSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget;
        const formData = new FormData(form);
        const data = {
            ...Object.fromEntries(formData.entries()),
            Usernames: selectedUsernames,
            PriorityId: priority?.id,
            CategoryId: category?.id
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

    async function GetAllPriorities() {
        const response = await fetch(`/ticket/GetPriorities`);
        if (response.ok) {
            setPriorities(await response.json());
        }
    }

    async function GetAllCategories() {
        const response = await fetch(`/ticket/GetCategories`);
        if (response.ok) {
            setCategories(await response.json());
        }
    }

    useEffect(() => {
        GetAllPriorities();
        GetAllCategories();
    }, [])

    const priorityOptions = priorities === undefined ? [] : priorities.map((priority) => ({
    value: priority.id,
    label: priority.prioritylabel,
    }));

    const categoryOptions = categories === undefined ? [] : categories.map((category) => ({
    value: category.id,
    label: category.categorylabel,
    }));

    const handlePriority = (priority : Priority) => {
        setPriority(priority);
    };

    const handleCategory = (category : Category) => {
        setCategory(category);
    };

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
    
    useEffect(() => {
        getUser();
    }, [username]);

    return (
        <form className="w-50 mx-auto d-flex flex-column form-group gap-2" onSubmit={handleTicketSubmit}>
            <label>Fill out ticket blanket</label>
            <input className="form-control" name="Title" placeholder="Enter ticket title" required />
            <input className="form-control" name="Description" placeholder="Enter ticket description" required />

            <Dropdown
                controlClassName="btn btn-outline-primary dropdown-toggle"
                menuClassName="list-group"
                optionClassName="list-group-item list-group-item-action"
                aria-label="Priority"
                options={priorityOptions}
                onChange={(option) => {
                const priority = priorities?.find(
                    (priority) => priority.id === option.value
                );

                if (priority) {
                    handlePriority(priority);
                }
                }}      
                placeholder="Select a priority"
            />
            <Dropdown
                controlClassName="btn btn-outline-primary dropdown-toggle"
                menuClassName="list-group"
                optionClassName="list-group-item list-group-item-action"
                aria-label="Category"
                options={categoryOptions}
                onChange={(option) => {
                const category = categories?.find(
                    (category) => category.id === option.value
                );

                if (category) {
                    handleCategory(category);
                }
                }}      
                placeholder="Select a category"
            />
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