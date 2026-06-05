# Smart Utube

# Opis projektu

Smart Utube to aplikacja webowa stworzona w ASP.NET Core MVC z dodatkiem Web API, umożliwiająca zarządzanie bazą filmów. System pozwala użytkownikom na przeglądanie filmów, ocenianie, komentowanie, zarządzanie playlistami oraz listą ulubionych. Administrator posiada uprawnienia do zarządzania filmami, kategoriami oraz komentarzami.

Aplikacja została zaprojektowana w architekturze warstwowej z wykorzystaniem wzorców projektowych takich jak Repository Pattern, Dependency Injection oraz Data Transfer Object (DTO).

---

# Live demo

https://smart-utube-app-bycuc6hqcpcxdebx.polandcentral-01.azurewebsites.net/

---

# Wykorzystane technologie i biblioteki

Backend
- ASP.NET Core MVC (.NET 8.0)
- ASP.NET Core Web API
- C# 12

Baza danych
- Entity Framework Core 8.0.0
- SQLite 8.0.0
- Microsoft.EntityFrameworkCore.Tools 8.0.0

Autoryzacja
- Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.0

Testy jednostkowe
- xUnit 2.9.3
- Moq 4.20.72
- Microsoft.NET.Test.Sdk 17.8.0

Generowanie PDF
- QuestPDF 2026.5.0

---

# Instalacja i konfiguracja projektu

1. Klonowanie repozytorium

git clone https://github.com/bethixx/Smart-Utube

2. Otworzenie projektu

Otwórz plik rozwiązania .sln w Visual Studio 2022 lub nowszym.

3. Konfiguracja bazy danych

Projekt używa SQLite oraz Entity Framework Core.

Wykonaj migracje: 
dotnet ef database update

4. Uruchomienie aplikacji

dotnet run
