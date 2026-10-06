import { useState } from 'react';
import { toast } from "sonner";

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
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="usernameInput">Username:</label>
        <input id="UserName" name="UserName" type="text" required/>
        <input id="Password" name="Password" type="password" required/>
      </div>
      {error.map((errorMsg) => {
        return(
        <p>{errorMsg}</p>
        )
      })}
      <button type="submit">Submit</button>
    </form>
  )
}


export default SignUpForm;