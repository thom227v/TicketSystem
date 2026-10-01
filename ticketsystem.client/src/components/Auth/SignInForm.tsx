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
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="usernameInput">Username:</label>
        <input id="UserName" name="UserName" type="text" />
        <input id="Password" name="Password" type="password" />
      </div>
      <button type="submit">Submit</button>
    </form>
  )
}


export default SignInForm;