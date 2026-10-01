import { useState, useEffect } from 'react';
import Dropdown, { type Option } from 'react-dropdown'

interface Department {
    id: number;
    name: string;
}


function Create() {

    const [departments, setDepartments] = useState<Department[]>();
    const [currentDepartment, setCurrentDepartment] = useState<Department>();

    async function handleTicketSubmit (event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const form = event.currentTarget
        let formData = new FormData(form)
        formData.append("department", JSON.stringify(currentDepartment));
        const data = Object.fromEntries(formData.entries());   

        const response = await fetch('/ticket/CreateServiceAgreement', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
        });
        if (response.ok) {
            alert("Service agreement created successfully");
        }};

        async function getDepartments() {
        const response = await fetch('/ticket/GetDepartments');
        if (response.ok) {
            const data = await response.json();
            setDepartments(data);
        }};

        useEffect(() => {
            getDepartments();
        }, []);

        async function handleDepartmentChange(e : Department)
        {
            setCurrentDepartment(e);
        }

        const options = departments === undefined ? [] : departments.map((department) => ({
        value: department.id,
        label: department.name,
        }));

    return (
        <>
            <form onSubmit={handleTicketSubmit}>
                <div>
                    <p>Create SLA</p>
                </div>
                <input name="titleId" placeholder="Enter service agreement title"></input>
                <input name="descriptionId" placeholder="Enter service agreement description"></input>
                <input name="priorityId" placeholder="Enter service agreement priority"></input>
                <input name="categoryId" placeholder="Enter service agreement category"></input>
                <Dropdown
                    aria-label="Department"
                    options={options}
                    onChange={(option) => {
                    const department = departments?.find(
                        (department) => department.id === option.value
                    );

                    if (department) {
                        handleDepartmentChange(department);
                    }
                    }}      
                    placeholder="Select a department"
                    />
                <button type="submit">Submit</button>
            </form>
        </>
    );
}

export default Create;