import { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router';
interface UserInfo {
  userName: string;
}


function UserInfoForm() {
  const [userInfo, setUserInfo] = useState<UserInfo | null>(null);
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    // Skip the auth check on the login page entirely
    if (location.pathname === '/Login') return;

    async function getUserInfo() {
      try {
        const response = await fetch('/auth/UserInfo');
        if (response.ok) {
          const data: UserInfo = await response.json();
          setUserInfo(data);
        } else {
          navigate('/Login');
        }
      } catch (error) {
        console.log(error);
      }
    }

    getUserInfo();
  }, [location.pathname, navigate]);

  return (
    <>
        {
            userInfo !== undefined && userInfo?.userName !== undefined ?
                <div className="nav-item flex-grow-1 d-flex justify-content-end">
                    <div className="nav-item border border-secondary rounded p-2 d-flex justify-content-center align-items-center">
                         <p className="m-0">Logged in as: {userInfo?.userName}</p>
                    </div>
                </div>
            :
            <></>
        }
        
    </>
  );
}

export default UserInfoForm;