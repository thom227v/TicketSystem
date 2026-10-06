import SignInForm from "../components/Auth/SignInForm";
import SignUpForm from "../components/Auth/SignUpForm";
import { useState } from 'react';
export default function HomePage() {

 const [logIn, setLogIn] = useState<boolean>(false);
 const handleLogIn = () => {
     if (logIn == true){
         setLogIn(false);
     }else{
         setLogIn(true);
     }
  }
  
  return (
    <main>
        {
        logIn === false 
        ? 
            <>
                <div className="text-center"> 
                    <h2>Account registration</h2>
                    <p>Please fill out the required fields.</p>
                    <SignUpForm/>
                    <button  className="w-25 align-self-center mt-2 btn btn-secondary" onClick={handleLogIn}>Already have a account? Sign in</button>
                </div>
            </>
        :
            <>
                <div className="text-center"> 
                    <h2>You are not signed in</h2>
                    <p>Please sign into your account.</p>
                    <SignInForm/>
                    <button  className="w-25 align-self-center mt-2 btn btn-secondary" onClick={handleLogIn}>Dont have an account? Sign up</button>
                </div>
            </>
        }
    </main>
  );
}