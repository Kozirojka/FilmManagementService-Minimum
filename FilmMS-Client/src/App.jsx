import Table from "./compontents/table/table";
import Modal from "./compontents/modal/AddEditModal/EditAddModal";
import DeleteModal from "./compontents/modal/DeleteModal/DeleteModal";
import BASE_URL from "./API/BASE_URL";

import { useState, useEffect} from "react";

function App() {

  const [films, setFilms] = useState([]);
  const [selectedFilm, setSelectedFilm] = useState(null);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);
  const [method, setMethod] = useState("");


  useEffect(() => {
    const fetchFilms = async () => {
      try {
        const res = await fetch(`${BASE_URL}/films`);
        const data = await res.json();
        setFilms(data);
      } catch (error) { 
        console.error("Fetch error:", error);
      }
    };
    fetchFilms();
  }, []);


  const handleSave = async (filmData) => {
    try {
      let url;
      let options;
  
      if (method === "edit") {
        url = `${BASE_URL}/films/${filmData.id}`;
        options = {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(filmData),
        };
      } else if (method === "add") {
        url = `${BASE_URL}/films`;

        const filmDataNew = { ...filmData };
        delete filmDataNew.id;

        options = {
          method: "POST", 
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(filmDataNew),
        };
      } else {
        throw new Error("Not supported method");
      }
      
      const response = await fetch(url, options);
  
      if (!response.ok) {
        throw new Error("not saved data");
      }
  
      const updatedFilms = await fetch("https://localhost:7091/films").then(r => r.json());
      setFilms(updatedFilms);
      setIsModalVisible(false);
    } catch (error) {
      console.error("Saving not complete:", error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await fetch(`${BASE_URL}/films/${id}`, { method: "DELETE" });
      setFilms(prev => prev.filter(f => f.id !== id));
      setIsDeleteModalVisible(false);
    } catch (error) {
      console.error("Delete error:", error);
    }
  };


  return (
    <>
      <Table 
        data={films} 
        onModelOpen={(film, method) => {
          setSelectedFilm(film);
          setMethod(method);
          setIsModalVisible(true);
        }}
        onDeleteFilm={film => {
          setSelectedFilm(film);
          setIsDeleteModalVisible(true);
        }}
      />

      
      {isModalVisible && (
        <Modal onExit={() => setIsModalVisible(false)} method={method} film={selectedFilm} onSave={handleSave} />
      )}


      {isDeleteModalVisible && (
        <DeleteModal onExit={() => setIsDeleteModalVisible(false)} onConfirm={() => handleDelete(selectedFilm?.id)} />
      )}
    </>
  );
}

export default App;
