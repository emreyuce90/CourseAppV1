export const getToken = () => {
  const token = localStorage.getItem("jwt");
  if (token) {
    const data = JSON.parse(token);
    return data.token.token;
  }
};
