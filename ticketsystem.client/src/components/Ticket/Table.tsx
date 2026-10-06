import { useEffect, useState } from "react";
import TicketViewModal from "../Modals/TicketViewModal";

interface Ticket {
    id: number;
    submittedby: string;
    title: string;
    description: string;
    priority: number;
    category: number;
    affectedusers: string[];
}

function Table() {
    const [tickets, setTickets] = useState<Ticket[]>();
    const [viewTicket, setViewTicket] = useState<Ticket | null>(null);

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
                    <th>Affected User</th>
                    <th>View</th>
                </tr>
            </thead>
            <tbody>
                {tickets.map(ticket =>
                    <tr key={ticket.id} className=">w-50">
                        <td>{ticket.id}</td>
                        <td>{ticket.submittedby}</td>
                        <td>{ticket.title}</td>
                        <td>{ticket.description}</td>
                        <td>{ticket.priority}</td>
                        <td>{ticket.category}</td>
                        <td>{ticket.affectedusers.length < 2 ? ticket.affectedusers[0] : `${ticket.affectedusers[0]} +${ticket.affectedusers.length-1} `}</td>
                        <td><button type="button" onClick={() => setViewTicket(ticket)}>View</button></td>
                    </tr>
                )}
            </tbody>
        </table>}
        {viewTicket !== null &&
            <TicketViewModal ticket={viewTicket} show={true} onHide={() => setViewTicket(null)} />}
    </>

);
}

export default Table;