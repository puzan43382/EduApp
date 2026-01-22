let modebtn = document.querySelector("#mode");
let currmode = "light";
let body = document.querySelector("body");

modebtn.addEventListener("click", () => {
  if (currmode === "light") {
    currmode = "dark";
    body.classList.remove("light");
    body.classList.add("dark");
  } else {
    currmode = "light";
    body.classList.remove("dark");
    body.classList.add("light");
  }
  console.log('current mode is ', currmode);
});