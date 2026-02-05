document.addEventListener("DOMContentLoaded", () => {
  const form = document.querySelector("form");
  const errorDiv = document.createElement("div");
  errorDiv.className = "error-message";
  form.appendChild(errorDiv);

  form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const username = document.getElementById("user").value;
    const password = document.getElementById("password").value;

    try {
      const response = await fetch("https://localhost:5067/api/Login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
      });

      if (response.ok) {
        window.location.href = "index.html";
      } else {
        const data = await response.json();
        errorDiv.textContent = data.message || "Usuário ou senha incorretos!";
        errorDiv.style.color = "red";
        errorDiv.style.marginTop = "10px";
      }
    } catch (error) {
      console.error("Erro de conexão:", error);
      errorDiv.textContent = "Erro ao conectar com o servidor!";
      errorDiv.style.color = "red";
      errorDiv.style.marginTop = "10px";
    }
  });
});
