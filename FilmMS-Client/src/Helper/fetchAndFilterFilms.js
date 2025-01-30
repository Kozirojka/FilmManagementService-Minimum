// filmSearchHelper.js
import BASE_URL from "../API/BASE_URL";


export const fetchAndFilterFilms = async (searchValue, films, setFilteredFilms) => {
    if (searchValue.trim() === "") {
      setFilteredFilms(films);
      return;
    }
  
    const id = parseInt(searchValue, 10);
    if (!isNaN(id)) {
      try {
        const response = await fetch(`${BASE_URL}/films/${id}`);
        console.log(response);
  
        if (response.ok) {
          const film = await response.json();
          setFilteredFilms([film]);
        } else {
          setFilteredFilms([]);
        }
      } catch (error) {
        console.error("Error fetching film by ID:", error);
        setFilteredFilms([]);
      }
    } else {
      const query = searchValue.toLowerCase();
      const filtered = films.filter(
        (film) =>
          film.title.toLowerCase().includes(query) ||
          film.director.toLowerCase().includes(query)
      );
      setFilteredFilms(filtered);
    }
  };
  