import Create from "../components/Ticket/Create";
import Table from "../components/Ticket/Table";

export default function Ticket() {

    return (
        <>
            <meta name="description" content="Ticket administration" />
            <Create></Create>
            <Table></Table>
        </>
    );
}