async function fetchData(url) {
  try {
    const response = await fetch(url);

    if (!response.ok) {
      throw new Error("Something went wrong");
    }

    return await response.json();
  } catch (error) {
    console.error("Fetch error:", error);
  }
}
export { fetchData };