import SignInForm from "../components/Auth/SignInForm";
import SignUpForm from "../components/Auth/SignUpForm";

export default function HomePage() {

 const handleSignUpSuccess = () => {
    console.log("SIGN UP WAS SUCCESS")
  }
  
  return (
    <main>
        <div className="text-center"> 
            <h2>Account registration</h2>
            <p>Please fill out the required fields.</p>
        </div>
        <SignUpForm onSuccess={handleSignUpSuccess} />

        <div className="text-center"> 
            <h2>You are not signed in</h2>
            <p>Please sign into your account</p>
        </div>
        <SignInForm onSuccess={handleSignUpSuccess} />
    </main>
  );
}