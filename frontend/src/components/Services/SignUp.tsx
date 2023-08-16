import React, { useState, useEffect, useRef } from "react";
import { createOneUser } from "../../redux/reducers/usersReducer";
import useAppDispatch from "../../hooks/useAppDispatch";

const SignUp = () => {
  const dispatch = useAppDispatch();
  const [name, setName] = useState("");
  const defaultAvatar =
    "https://upload.wikimedia.org/wikipedia/fi/4/45/Yoda.jpg";
  const inputRef = useRef<HTMLInputElement | null>(null);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [age, setAge] = useState(-1);
  const [avatar, setAvatar] = useState(defaultAvatar);
  const [error, setError] = useState("");
  // const [createdNewUserSucess, setCreatedNewUserSucess] = useState(false);
//   const { newUser } = useAppSelector((state) => state.usersReducer);
//   const dispatch = useAppDispatch();

  // const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
  //   e.preventDefault();
  //   if (!name || !email) {
  //     setError("Name and email cannot be empty");
  //   } else if (name.length < 4) {
  //     setError("Username must be at least 4 characters");
  //   } else if (password.length < 5) {
  //     setError("Password must be at least 5 characters long.");
  //   } else {
  //     if (
  //       inputRef.current &&
  //       inputRef.current.files &&
  //       inputRef.current.files.length > 0
  //     ) {
  //       const avatarFile = inputRef.current.files[0];
  //     }

  //   //   dispatch(createOneUser({ name, email, password, avatar }));
  //   //   if (newUser) {
  //   //     alert("User issuccessfully created! Please login");
  //   //     window.location.href = "/login";
  //   //   }
      
  //   }
  // };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    // ... (validation checks and other logic)

    try {
      await dispatch(createOneUser({ name, email, password, avatar, age: -1}));
      // User created successfully, you might want to navigate to a new page or show a success message
      alert("User successfully created! Please login");
      window.location.href = "/login";
    } catch (error) {
      // Handle error, maybe show an error message
      console.error("Error creating user:", error);
    }
  };

  const clearAvatar = () => {
    setAvatar(defaultAvatar);
    if (inputRef.current) {
      inputRef.current.value = "";
    }
  };

//   const handleSubmit = async () => {
//     const { name, email, password, avatar } = formData; // Get your form data
    
//     try {
//         // Dispatch the createOneUser action
//         await dispatch(createOneUser({ name, email, password, avatar }));
//         // User created successfully, you might want to navigate to a new page or show a success message
//     } catch (error) {
//         // Handle error, maybe show an error message
//     }
// };

  return (
    <div data-testid="signup">
      <form onSubmit={(e) => handleSubmit(e)}>
        <input
          type="text"
          name="username"
          placeholder="Username"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="password"
          name="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <input
          type="number"
          name="age"
          placeholder="age"
          value={age}
          onChange={(e) => setAge(parseInt(e.target.value, 10))}
        />
        <button> Submit form </button>
      </form>
      {error && <p>{error}</p>}
    </div>
  );
};

export default SignUp;

