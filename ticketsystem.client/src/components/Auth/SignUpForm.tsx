import { useState, useEffect } from 'react';
import { toast } from "sonner";
import Dropdown from 'react-dropdown'
import type { Option } from 'react-dropdown';
import Department from '../../pages/Department';

interface Department {
    id: number;
    name: string;
}

function SignUpForm() {
    const [error, setError] = useState<string[]>([]);
    const [departments, setDepartments] = useState<Department[]>();
    const [department, setDepartment] = useState<Option | null>(null);

  async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
    event.preventDefault()

    if (department === null) {
        setError(["Please select a department"]);
        return;
    }

    const form = event.currentTarget
    const formData = new FormData(form)
    //const username = String(formData.get('usernameInput') ?? '')
    const data = Object.fromEntries(formData.entries());

    //const response = 
    const response = await fetch('/auth/SignUp', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (response.ok) {
        toast.success("You are now signed up");
    }else
    {
        setError(await response.json())
    }
  }

    useEffect(() => {
        async function getDepartments() {
            const response = await fetch('/department/GetDepartments');
            if (response.ok) {
                const data = await response.json();
                setDepartments(data);
        }};
        getDepartments();
    }, []);

  return (
    <form className="w-25 mx-auto d-flex flex-column gap-2" onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="username">Username</label>
          <input id="username" className="form-control"  name="UserName" type="text" required/>
        </div>
        <div className="form-group">
          <label htmlFor="password">Username</label>
          <input id="password" className="form-control" name="Password" type="password" required/>
        </div>
        <Dropdown
            name="DepartmentId"
            controlClassName="btn btn-outline-primary dropdown-toggle"
            menuClassName="list-group"
            optionClassName="list-group-item list-group-item-action"
            aria-label="Number"
                value={department}
                onChange={setDepartment}
            options={
                departments === undefined ? [] : departments.map((department) => ({
                    value: department.id,
                    label: department.name,
                 }))
            }
            
            placeholder="Select a department"
        />
        <button className="w-100 align-self-center mt-2 btn btn-primary" type="submit">Sign up</button>
        {Array.isArray(error) && error.map((errorMsg) => {
          return(
          <p className="text-danger">{errorMsg}</p>
          )
        })}
        </form>
  )
}


export default SignUpForm;