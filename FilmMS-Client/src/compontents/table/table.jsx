  import { useEffect, useState } from "react";
  import Button from "@mui/material/Button";
  import EditIcon from "@mui/icons-material/Edit";
  import DeleteIcon from "@mui/icons-material/Delete";
  import AddIcon from "@mui/icons-material/Add";
  import ArrowBackIcon from "@mui/icons-material/ArrowBack";
  import ArrowForwardIcon from "@mui/icons-material/ArrowForward";

  const Table = ({ onModelOpen, onDeleteFilm }) => {

    
    const [films, setFilms] = useState([]);
    const [searchValue, setSearchValue] = useState("");
    const [page, setPage] = useState(1);
    const pageSize = 5;

    const startIndex = page * pageSize;
    const endIndex = startIndex + pageSize;

    const paginatedFilms = films.slice(startIndex, endIndex);

    const handlePrevious = () => {
      setPage((prevPage) => prevPage - 1);
    }


    const handleNext = () => {
      setPage((prevPage) => prevPage + 1);
    }


    const getFilms = async () => {
      try {
        const result = await fetch("https://localhost:7091/films");
        const data = await result.json();
        setFilms(data);
      } catch (error) {
        console.error("Error fetching films:", error);
      } finally {
        console.log("Films fetched");
      }
    };
    
    useEffect(() => {
      getFilms();
    }, []);

    const filteredFilms = films.filter((film) => {
      const query = searchValue.toLowerCase();
      return (
        film.title.toLowerCase().includes(query) ||
        film.director.toLowerCase().includes(query)
      );
    });

    return (
      <>
        <Button color="secondary" onClick={() => onModelOpen(null, "add")}>
          <AddIcon />
        </Button>

        <input
          type="text"
          value={searchValue}
          placeholder="Search by title or director"
          onChange={(e) => setSearchValue(e.target.value)}
        />

        <table>
          <thead>
            <tr>
              <th>Title</th>
              <th>Genre</th>
              <th>Director</th>
              <th>Release</th>
              <th>Rating</th>
              <th>Descretion</th>
              <th>Option</th>
            </tr>

            {paginatedFilms.map((film) => {
              return (
                <tr key={film.id}>
                  <td>{film.title}</td>
                  <td>{film.genre}</td>
                  <td>{film.director}</td>
                  <td>{film.release}</td>
                  <td>{film.rating}</td>
                  <td>{film.description}</td>
                  <td>
                    <Button
                      variant="outlined"
                      size="small"
                      color="error"
                      startIcon={<EditIcon />}
                      onClick={() => onModelOpen(film, "edit")}
                    />
                  </td>
                  <td>
                    <Button
                      variant="outlined"
                      size="small"
                      startIcon={<DeleteIcon />}
                      onClick={() => onDeleteFilm(film)}
                    />
                  </td>
                </tr>
              );
            })}
          </thead>
        </table>

        <div>
          <Button onClick={handlePrevious} disabled={page === 0} startIcon={<ArrowBackIcon/>}/>
          <Button onClick={handleNext} disabled={filteredFilms.length < endIndex} startIcon={<ArrowForwardIcon/>}/>
        </div>
      </>
    );
  };

  export default Table;
