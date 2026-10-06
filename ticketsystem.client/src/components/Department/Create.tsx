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
            <form className="w-50 mx-auto d-flex flex-column form-group gap-2" onSubmit={handleTicketSubmit}>
                <label>Create Department</label>
                <input className="form-control" name="name" placeholder="Enter department name" required></input>
                <button className="w-50 align-self-center btn btn-primary" type="submit">Submit</button>
            </form>
        </>
    );
}

export default Create;