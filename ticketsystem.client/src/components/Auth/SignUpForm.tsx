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
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="usernameInput">Username:</label>
        <input id="UserName" name="UserName" type="text" />
        <input id="Password" name="Password" type="password" />
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