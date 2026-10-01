import { useEffect, useState } from "react";

interface ServiceAgreementWithDepartmentName {
    id: number;
    departmentName: string;
    title: string;
    description: string;
    createdby: string;
    signedby: string;
}

function Table() {

    const [serviceAgreements, setServiceAgreements] = useState<ServiceAgreementWithDepartmentName[]>();

    async function GetServiceAgreements() {
    const response = await fetch('/serviceagreement/GetServiceAgreements');
    if (response.ok) {
        const data = await response.json();
        setServiceAgreements(data);
    }};

    useEffect(() => {
        GetServiceAgreements();
    }, []);

return (
    <>
        {serviceAgreements === undefined ? <p>currently no service agreements</p> : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Department name</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Created By</th>
                    <th>Signed By</th>
                </tr>
            </thead>
            <tbody>
                {serviceAgreements.map(serviceAgreement =>
                    <tr key={serviceAgreement.id}>
                        <td>{serviceAgreement.id}</td>
                        <td>{serviceAgreement.departmentName}</td>
                        <td>{serviceAgreement.title}</td>
                        <td>{serviceAgreement.description}</td>
                        <td>{serviceAgreement.createdby}</td>
                        <td>{serviceAgreement.signedby}</td>
                    </tr>
                )}
            </tbody>
        </table>};
    </>

);
}

export default Table;