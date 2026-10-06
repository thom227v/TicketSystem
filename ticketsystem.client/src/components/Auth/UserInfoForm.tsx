import { useEffect, useState } from 'react';

type SignUpFormProps = {
  onSuccess: () => void;
};

interface UserInfo {
  userName: string;
}

function UserInfoForm({ onSuccess }: SignUpFormProps) {
  const [userInfo, setUserInfo] = useState<UserInfo | null>(null);

  useEffect(() => {
    async function getUserInfo() {
      try {
        const response = await fetch('/auth/UserInfo');
        if (response.ok) {
          const data: UserInfo = await response.json();
          setUserInfo(data);
          console.log(userInfo);
          onSuccess();
        }
      } catch (error) {
        console.error('Failed to get UserInfo ', error);
      }
    }

    getUserInfo();
  }, []);

  return (
    <div>
      <p>{userInfo?.userName}</p>
    </div>
  );
}

export default UserInfoForm;