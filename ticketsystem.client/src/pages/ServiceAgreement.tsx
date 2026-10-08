import Table from "../components/ServiceAgreement/Table";
import Create from "../components/ServiceAgreement/Create";

export default function ServiceAgreement() {
    return (
        <>
            <meta name="description" content="ServiceAgreement administration" />
            <Create></Create>
            <Table></Table>
        </>
    );
}