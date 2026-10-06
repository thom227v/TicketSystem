import { toast } from "sonner";

function Create() {

    async function handleTicketSubmit (event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget
        const formData = new FormData(form)
        const data = Object.fromEntries(formData.entries());   

        const response = await fetch('/department/CreateDepartment', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
        });

        if (response.ok) {
            toast.success("Department created successfully");
        }};

    return (
        <>
            <form onSubmit={handleTicketSubmit}>
                <div>
                    <p>Create Department</p>
                </div>
                <input name="name" placeholder="Enter department name" required></input>
                <button type="submit">Submit</button>
            </form>
        </>
    );
}

export default Create;