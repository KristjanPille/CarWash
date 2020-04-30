<template>
<div>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">
                <router-link to="" a class="navbar-brand">Home</router-link>
                <button
                class="navbar-toggler"
                type="button"
                data-toggle="collapse"
                data-target=".navbar-collapse"
                aria-controls="navbarSupportedContent"
                aria-expanded="false"
                aria-label="Toggle navigation"
                >
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Identity/Account/Register">Register</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Identity/Account/Login">Login</a>
                        </li>

                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Language
                            </a>
                            <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <a class="dropdown-item" href="/Home/SetLanguage?culture=en-GB&amp;returnUrl=%2F">English (United Kingdom)</a>
                                <a class="dropdown-item" href="/Home/SetLanguage?culture=et-EE&amp;returnUrl=%2F">eesti (Eesti)</a>
                            </div>
                        </li>
                    </ul>

                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <router-link to="/" a class="nav-link text-dark">Home</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link to="/persons" a class="nav-link text-dark">Persons</router-link>
                        <li class="nav-item">
                            <router-link to="/cars" a class="nav-link text-dark">cars</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link to="/personcars" a class="nav-link text-dark">PersonCars</router-link>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/CarTypes/Create">CarType</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Checks/Create">Check</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Discounts/Create">Discount</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/IsInWashs/Create">IsinWash</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/ModelMarks/Create">ModelMark</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Orders/Create">Orders</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/PaymentMethods/Create">PaymentMethod</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Payments/Create">Payment</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Persons/Create">Person</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/PersonCars/Create">PersonCar</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Services/Create">Service</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/WashTypes/Create">WashType</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" href="/Washs/Create">Washes</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
            <router-view />
        </main>
    </div>
</div>
</template>
