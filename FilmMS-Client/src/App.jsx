import Table from "./compontents/table/table";
import Modal from "./compontents/modal/AddEditModal/EditAddModal";
import DeleteModal from "./compontents/modal/DeleteModal/DeleteModal";
import BASE_URL from "./API/BASE_URL";
import ErrorModal from "./compontents/errorMessage/ErrorModal";
import { saveFilm } from "./API/filmService";

import { fetchAndFilterFilms } from "./Helper/fetchAndFilterFilms";

import { useState, useEffect } from "react";

function App() {
  const [films, setFilms] = useState([]);
  const [filteredFilms, setFilteredFilms] = useState([]);
  const [searchValue, setSearchValue] = useState("");
  const [selectedFilm, setSelectedFilm] = useState(null);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);
  const [method, setMethod] = useState("");
  const [errors, setErrors] = useState([]);

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

  useEffect(() => {
    const handler = setTimeout(() => {
      fetchAndFilterFilms(searchValue, films, setFilteredFilms);
    }, 500);

    return () => clearTimeout(handler);
  }, [searchValue, films]);

  const handleSave = async (filmData) => {
    try {


      await saveFilm(filmData, method);

      const updatedFilms = await fetch(`${BASE_URL}/films`).then((r) =>
        r.json()
      );

      setFilms(updatedFilms);
      setIsModalVisible(false);
    } catch (error) {
      try{
        const parsedError = JSON.parse(error.message);
        setErrors(parsedError.errors);
      // eslint-disable-next-line no-unused-vars
      }catch(parseError){
        console.error("Unexpected error format", error);
      }
  };
  }

  const handleDelete = async (id) => {
    try {
      await fetch(`${BASE_URL}/films/${id}`, { method: "DELETE" });
      setFilms((prev) => prev.filter((f) => f.id !== id));
      setIsDeleteModalVisible(false);
    } catch (error) {
      console.error("Delete error:", error);
    }
  };

  return (
    <>
      <Table
        data={filteredFilms}
        onModelOpen={(film, method) => {
          setSelectedFilm(film);
          setMethod(method);
          setIsModalVisible(true);
        }}
        onDeleteFilm={(film) => {
          setSelectedFilm(film);
          setIsDeleteModalVisible(true);
        }}
        searchValue={searchValue}
        onSearchChange={setSearchValue}
      />

      {isModalVisible && (
        <Modal
          onExit={() => setIsModalVisible(false)}
          method={method}
          film={selectedFilm}
          onSave={handleSave}
        />
      )}

      {isDeleteModalVisible && (
        <DeleteModal
          onExit={() => setIsDeleteModalVisible(false)}
          onConfirm={() => handleDelete(selectedFilm?.id)}
          onSearchChange={setSearchValue}
        />
      )}

      <ErrorModal errors={errors} onClose={() => setErrors([])} />
    </>
  );
}

export default App;
