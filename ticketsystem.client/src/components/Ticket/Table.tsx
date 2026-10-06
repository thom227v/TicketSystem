import { useEffect, useState } from "react";

interface Ticket {
    id: number;
    submittedby: string;
    title: string;
    description: string;
    priority: number;
    category: number;
}

function Table() {

    const [tickets, setTickets] = useState<Ticket[]>();

    async function GetTickets() {
    const response = await fetch('/ticket/GetTickets');
    if (response.ok) {
        const data = await response.json();
        setTickets(data);
    }};

    useEffect(() => {
        GetTickets();
    }, []);

return (
    <>
        {tickets === undefined ? <p>currently no tickets</p> : <table className="mt-4 w-75 mx-auto table table-light table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Submitted By</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Priority</th>
                    <th>Category</th>
                </tr>
            </thead>
            <tbody>
                {tickets.map(ticket =>
                    <tr key={ticket.id}>
                        <td>{ticket.id}</td>
                        <td>{ticket.submittedby}</td>
                        <td>{ticket.title}</td>
                        <td>{ticket.description}</td>
                        <td>{ticket.priority}</td>
                        <td>{ticket.category}</td>
                    </tr>
                )}
            </tbody>
        </table>}
    </>

);
}

export default Table;