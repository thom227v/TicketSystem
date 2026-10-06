import { toast } from "sonner";

function SignInForm() {
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
        toast.success("You are now signed in");
    }
  }

  return (
<form className="w-25 mx-auto d-flex flex-column" onSubmit={handleSubmit}>
        <div className="form-group">
            <label >Username</label>
            <input className="form-control"  name="UserName" type="text" required/>
        </div>
        <div className="form-group">
             <label>Password</label>
             <input className="form-control" name="Password" type="password" required/>
        </div>
        <button className="w-100 align-self-center mt-2 btn btn-primary" type="submit">Sign in</button>
    </form>
  )
}


export default SignInForm;