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
    <div>
      <p>{userInfo?.userName}</p>
    </div>
  );
}

export default UserInfoForm;