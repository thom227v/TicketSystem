import { useState } from 'react';
import { toast } from "sonner";
import Dropdown from 'react-dropdown'

type SignUpFormProps = {
  onSuccess: () => void
}

function SignUpForm({ onSuccess }: SignUpFormProps) {
    const [error, setError] = useState([]);

  async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
    event.preventDefault()
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
        onSuccess();
        toast.success("Du er nu signed up");
    }else
    {
        setError(await response.json())
    }
  }

  return (
    <form className="w-25 mx-auto d-flex flex-column" onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Username</label>
          <input className="form-control"  name="UserName" type="text" required/>
        </div>
        <div className="form-group">
          <label>Username</label>
          <input className="form-control" name="Password" type="password" required/>
        </div>
        {error.map((errorMsg) => {
          return(
          <p>{errorMsg}</p>
          )
        })}

        <div className="form-group">
          <label>Select department</label>
          <Dropdown
            controlClassName="btn btn-outline-primary dropdown-toggle"
            menuClassName="list-group"
            optionClassName="list-group-item list-group-item-action"
            aria-label="Number"
            options={
                [
                    {value: '1', label: 'One'},
                    {value: '2', label: 'Two'}
                ]
            }
            
            placeholder="Select an option"
        />
        </div>
        <button className="w-100 align-self-center mt-2 btn btn-primary" type="submit">Sign up</button>
    </form>
  )
}


export default SignUpForm;