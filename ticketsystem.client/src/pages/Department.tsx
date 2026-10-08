import Table from "../components/Department/Table";
import Create from "../components/Department/Create";

export default function Department() {
    return (
        <>
            <meta name="description" content="Department administration" />
            <Create></Create>
            <Table></Table>
        </>
    );
}