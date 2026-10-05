type SignUpFormProps = {
  onSuccess: () => void
}
function SignInForm({ onSuccess }: SignUpFormProps) {
  async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
    event.preventDefault()
    const form = event.currentTarget
    const formData = new FormData(form)
    //const username = String(formData.get('usernameInput') ?? '')
    const data = Object.fromEntries(formData.entries());

    //const response = 
    const response = await fetch('/auth/SignIn', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    if (response.ok) {
        onSuccess();
    }
  }

  return (
    <form className="w-100" onSubmit={handleSubmit}>
    <div className="form-group">
     <label >Username</label>
     <input className="form-control"  name="UserName" type="text" />
    </div>
     <div className="form-group">
     <label>Password</label>
      <input className="form-control" name="Password" type="password" />
    </div>
    <button className="btn btn-primary" type="submit">Submit</button>
    </form>
  )
}


export default SignInForm;