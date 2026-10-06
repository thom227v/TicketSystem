import { useState } from 'react';

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
    }else
    {
        setError(await response.json())
    }
  }

  return (
    <form className="w-25 mx-auto d-flex flex-column" onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Username</label>
          <input className="form-control"  name="UserName" type="text" />
        </div>
        <div className="form-group">
          <label>Username</label>
          <input className="form-control" name="Password" type="password" />
        </div>
        {error.map((errorMsg) => {
          return(
          <p>{errorMsg}</p>
          )
        })}
        <button className="w-50 align-self-center mt-2 btn btn-primary" type="submit">Submit</button>
    </form>
  )
}


export default SignUpForm;