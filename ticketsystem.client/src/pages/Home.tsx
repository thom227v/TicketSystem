import SignInForm from "../components/Auth/SignInForm";
import SignUpForm from "../components/Auth/SignUpForm";

export default function HomePage() {



 const handleSignUpSuccess = () => {
    console.log("SIGN UP WAS SUCCESS")
  }



  return (
    <main>
      <h1 className="text-center">Welcome</h1>
      <SignUpForm onSuccess={handleSignUpSuccess} />
      <SignInForm onSuccess={handleSignUpSuccess} />
    </main>
  );
}