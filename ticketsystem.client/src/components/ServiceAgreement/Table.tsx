import { useEffect, useState } from "react";

interface Department {
    id: number;
    name: string;
}


interface ServiceAgreementWithDepartmentName {
    id: number;
    department: Department;
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
        {serviceAgreements === undefined ? <p>currently no service agreements</p> : <table className="mt-4 w-75 mx-auto table table-light table-striped" aria-labelledby="tableLabel">
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
                        <td>{serviceAgreement.department.name}</td>
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