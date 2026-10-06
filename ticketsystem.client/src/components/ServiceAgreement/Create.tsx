import { useState, useEffect } from 'react';
import Dropdown from 'react-dropdown'
import { toast } from "sonner";


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

            const response = await fetch('/serviceagreement/CreateServiceAgreement', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
            });
            if (response.ok) {
                toast.success("Service agreement created successfully");
            }};

        async function getDepartments() {
        const response = await fetch('/department/GetDepartments');
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
                <input name="Title" placeholder="Enter service agreement title" required></input>
                <input name="Description" placeholder="Enter service agreement description" required></input>
                <input name="Priority" placeholder="Enter service agreement priority" required></input>
                <input name="Category" placeholder="Enter service agreement category" required></input>
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