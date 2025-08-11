"use client";

import React, { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import IconButton from "@mui/material/IconButton";
import SearchIcon from "@mui/icons-material/Search";
import { useSearch } from "../SearchContext/SearchContext";

import HeartIcon from "@mui/icons-material/FavoriteBorderOutlined";
//import homeIcon from "@/app/assets/GymBeast.svg";
import CloseIcon from "@mui/icons-material/Close";

interface SearchDropdownProps {
  toggleDropdown: () => void;
  isVisible: boolean;
}

const SearchDropdown = ({
  toggleDropdown,
  isVisible,
}: SearchDropdownProps) => {
  const { searchTerm, setSearchTerm, filteredProducts } = useSearch();
  const router = useRouter();
  const [recentSearches, setRecentSearches] = useState<string[]>([]);
  const [showNoResults, setShowNoResults] = useState(false);

  useEffect(() => {
    const storedSearches = JSON.parse(
      localStorage.getItem("recentSearches") || "[]"
    );
    setRecentSearches(storedSearches);
  }, []);

  useEffect(() => {
    if (!searchTerm.trim()) {
      setShowNoResults(false); // reset no results when search is cleared
      return;
    }

    const timeoutId = setTimeout(() => {
      if (filteredProducts.length === 0) {
        setShowNoResults(true);
      }
    }, 500); //(500ms)

    return () => clearTimeout(timeoutId);
  }, [searchTerm, filteredProducts]);

  const handleSearch = (searchQuery: string) => {
    if (!searchQuery.trim()) return;

    const matchedProduct = filteredProducts.find((product) =>
      product.name.toLowerCase().includes(searchQuery.toLowerCase())
    );

    toggleDropdown(); // Close the dropdown

    if (matchedProduct) {
      const route = matchedProduct.category.toLowerCase();
      router.push(`/${route}/${matchedProduct._id}`);
    } else {
      // Navigate to search page if no searchword is matching
      router.push(`/search?q=${encodeURIComponent(searchQuery)}`);
    }

    setSearchTerm(searchQuery);
    const updatedSearches = [searchQuery, ...recentSearches].slice(0, 3); //stores last 3 searches
    localStorage.setItem("recentSearches", JSON.stringify(updatedSearches));
    setRecentSearches(updatedSearches);
  };

  const handleProduct = (productId: string, category: string) => {
    toggleDropdown();
    setSearchTerm("");
    const route = category.toLowerCase();
    router.push(`/${route}/${productId}`);
  };

  const handlecloseDropdown = () => {
    toggleDropdown();
    setSearchTerm("");
  };

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === "Enter") {
      e.preventDefault(); // Prevent page from reloading
      handleSearch(searchTerm);
    }
  };


  return (
    <div
      className={`${"fixed top-0 left-0 w-screen max-h-75 bg-white z-2 p-5 -translate-y-full transition-opacity duration-300 ease-in-out"} ${
        isVisible ? "max-h-175 opacity-1 translate-y-0" : ""
      }`}
    >

 
      <div className="grid grid-cols-3 gap-12.5 border-b-gray-500 w-1/2">
        <div className="-mt-2.5">
          <img className="w-25 h-25 ml-12.5 flex" alt="Home" />
        </div>
        <Paper
          component="form"
          className="bg-gray-400 w-50"
          sx={{
            p: "2px 4px",
            display: "flex",
            alignItems: "center",
            width: 400,
            backgroundColor: "#f4f4f4",
            boxShadow: "none",
            borderRadius: "20px",
          }}
        >
          <IconButton sx={{ p: "10px" }} aria-label="menu">
            <SearchIcon />
          </IconButton>
          <InputBase
            sx={{ ml: 1, flex: 1 }}
            placeholder="What are you looking for?"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            onKeyDown={handleKeyDown}
            inputProps={{ "aria-label": "search clothes" }}
          />
        </Paper>

        <div className="font-light flex justify-around items-start">
          <div
            className="flex cursor-pointer ml-auto mr-22.5"
            onClick={handlecloseDropdown}
          >
            <CloseIcon className="h-10 w-10 block font-light"></CloseIcon>
          </div>
        </div>
      </div>

      <div className="grid grid-cols-[repeat(auto-fill,minmax(300px,1fr))] gap-5 w-full p-5">
        {searchTerm && filteredProducts.length === 0 && showNoResults && (
          <div className="ml-12.5">
            <h2 className="size-5">No results found</h2>
            <p className="size-3.5">
            We didn&apos;t find anything for &quot;{searchTerm}&quot;
            </p>
          </div>
        )}

        {searchTerm &&
          filteredProducts.map((product) => (
            <div key={product._id} onClick={() => handleProduct(product._id, product.category)}>
              <div className="">

                {product.image.map((imgUrl, index) => (
                  <img
                    key={index}
                    src={imgUrl}
                    alt={product.name}
                    className=""
                  />
                ))}
              </div>
              <h2 className="">{product.name}</h2>
              <p className="">{product.price} kr</p>
            </div>
          ))}

        {!searchTerm && recentSearches.length > 0 && (
          <div className="ml-12.5 flex-col flex items-start">
            <p className="size-5 mb-2.5">Recent Searches:</p>
            {recentSearches.map((search, index) => (
              <li
                key={index}
                onClick={() => handleSearch(search)}
                className="flex items-center ml-0 gap-2 list-none leading-9"
              >
                <SearchIcon />
                {search}
              </li>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default SearchDropdown;
