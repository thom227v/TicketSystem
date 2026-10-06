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
        const formData = new FormData(form)
        const data = Object.fromEntries(formData.entries());   
        data.department = currentDepartment;

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
           <form className="w-50 mx-auto d-flex flex-column form-group gap-2" onSubmit={handleTicketSubmit}>
                <label>Create SLA</label>
                <input className="form-control" name="Title" placeholder="Enter service agreement title" required></input>
                <input className="form-control" name="Description" placeholder="Enter service agreement description" required></input>
                <Dropdown
                    controlClassName="btn btn-outline-primary dropdown-toggle"
                    menuClassName="list-group"
                    optionClassName="list-group-item list-group-item-action"
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
                <button className="mt-1 btn btn-primary" type="submit">Submit</button>
            </form>
        </>
    );
}

export default Create;