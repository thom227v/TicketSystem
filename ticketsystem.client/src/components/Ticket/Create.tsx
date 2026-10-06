function Create() {

    async function handleTicketSubmit (event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget
        const formData = new FormData(form)
        const data = Object.fromEntries(formData.entries());   

        const response = await fetch('/ticket/CreateTicket', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
        });
        if (response.ok) {
            alert("Ticket created successfully");
        }};

    return (
        <>
            <form className="w-50 mx-auto d-flex flex-column form-group" onSubmit={handleTicketSubmit}>
                <label>Fill out ticket blanket</label>
                <input className="form-control" name="Title" placeholder="Enter ticket title"></input>
                <input className="form-control" name="Description" placeholder="Enter ticket description"></input>
                <input className="form-control" name="Priority" placeholder="Enter ticket priority"></input>
                <input className="form-control" name="Category" placeholder="Enter ticket category"></input>
                <button className="mt-1 btn btn-primary" type="submit">Create Ticket</button>
            </form>
        </>
    );
}

export default Create;