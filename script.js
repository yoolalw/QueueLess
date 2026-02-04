document.addEventListener("DOMContentLoaded", () => {
  const form = document.querySelector("form");
  const errorDiv = document.createElement("div");
  errorDiv.className = "error-message";
  form.appendChild(errorDiv);

  form.addEventListener("submit", function(event) {
    event.preventDefault();

    const username = document.getElementById("user").value;
    const password = document.getElementById("password").value;

    const correctUsername = "admin";
    const correctPassword = "12345678";

    if (username === correctUsername && password === correctPassword) {
      window.location.href = "http://localhost:8080/swagger-ui/index.html";
    } else {
      errorDiv.textContent = "Usuário ou senha incorretos!";
      errorDiv.style.color = "red";
      errorDiv.style.marginTop = "10px";
    }
  });
});
