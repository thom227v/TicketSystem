import { useEffect, useState } from "react";

interface Department {
    id: number;
    name: string;
}

function Table() {

    const [departments, setDepartments] = useState<Department[]>();

      async function getDepartments() {
        const response = await fetch('/ticket/GetDepartments');
        if (response.ok) {
            const data = await response.json();
            setDepartments(data);
        }};

        useEffect(() => {
            getDepartments();
        }, []);

    return (
    <>
        {departments === undefined ? <p>currently no departments</p> : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Department name</th>
                </tr>
            </thead>
            <tbody>
                {departments.map(department =>
                    <tr key={department.id}>
                        <td>{department.id}</td>
                        <td>{department.name}</td>
                    </tr>
                )}
            </tbody>
        </table>};
    </>

);
}
export default Table;