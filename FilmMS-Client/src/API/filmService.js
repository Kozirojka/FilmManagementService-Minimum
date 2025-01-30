import BASE_URL from "./BASE_URL";

export async function saveFilm(filmData, method) {
    let url;
    let options;
  
    if (method === "edit") {
      url = `${BASE_URL}/films/${filmData.id}`;
      options = {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(filmData),
      };
    } else if (method === "add") {
      url = `${BASE_URL}/films`;
  
      const filmDataNew = { ...filmData };
      delete filmDataNew.id;
  
      options = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(filmDataNew),
      };
    } else {
      throw new Error("Not supported method");
    }
  
    const response = await fetch(url, options);
    const data = await response.json();
  
    if (!response.ok) {
      throw new Error(JSON.stringify(data));
    }
  
    return data;
  }
  