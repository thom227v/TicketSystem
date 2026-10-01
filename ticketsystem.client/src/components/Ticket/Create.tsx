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
            <form onSubmit={handleTicketSubmit}>
                <div>
                    <p>Create Ticket</p>
                </div>
                <input name="titleId" placeholder="Enter ticket title"></input>
                <input name="descriptionId" placeholder="Enter ticket description"></input>
                <input name="priorityId" placeholder="Enter ticket priority"></input>
                <input name="categoryId" placeholder="Enter ticket category"></input>
                <button type="submit">Submit</button>
            </form>
        </>
    );
}

export default Create;