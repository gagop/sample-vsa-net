# sample-vsa-net

## Opis (PL)

Ten projekt prezentuje podejście VSA (Vertical Slice Architecture) w środowisku .NET. Celem jest pokazanie, jak organizować kod wokół pionowych wycinków funkcjonalności, a nie warstw technicznych.

### Główne założenia VSA
- **Kolokacja powiązanych elementów**: Maksymalna kolokacja kodu należącego do jednej funkcji/biznesowego przypadku użycia. Zamiast dzielić aplikację na globalne warstwy (Controllers/Services/Repositories), dzielimy ją na **moduły** i **końcówki** (endpoints) wewnątrz modułów.
- **Moduły najpierw**: Aplikacja jest dzielona na moduły (np. `Auth`, `Comments`, `Groups`) w katalogu `VsaSample/Features/`. Każdy moduł zawiera swoje komendy, zapytania, handlery oraz końcówki.
- **Wynoszenie współdzielonych elementów**: Gdy pojawia się potrzeba ponownego użycia elementów w wielu miejscach, można je stopniowo wynosić wyżej – np. do katalogu `VsaSample/Shared/` (wspólne `Endpoints`, `Middlewares`, `Responses`) lub do infrastruktury (`VsaSample/Infrastructure/`).

### Opis projektu
- **Technologia**: .NET (minimal APIs / Web API).
- **Struktura**: Główna logika funkcjonalna w `VsaSample/Features/` (np. `Auth`, `Comments`, `Groups`), wspólne elementy w `VsaSample/Shared/`, warstwa danych w `VsaSample/Infrastructure/` (np. `Data`, `Entities`).
- **Przykładowe funkcje**: logowanie/rejestracja użytkownika (`Auth`), komentarze (`Comments`), grupy użytkowników (`Groups`).

### Struktura repo/projektu
```
sample-vsa-net/
  VsaSample/
    Features/
      Auth/
      Comments/
      Groups/
    Infrastructure/
      Data/
        Context/ (np. AppDbContext)
        Entities/
    Shared/
      Endpoints/
      Middlewares/
      Responses/
    Program.cs
```

### Baza danych
- Domyślnie używany jest **EF Core InMemory Database** (`UseInMemoryDatabase("TestDb")`).
- Nie wymaga ustawiania connection stringów – uruchomisz projekt bez dodatkowej konfiguracji.
- Uwaga: provider InMemory nie jest relacyjny (nie wymusza relacyjnych ograniczeń). Jeśli potrzebujesz semantyki relacyjnej w pamięci, rozważ **SQLite In-Memory**.

### Zalety
- **Silne powiązanie kodu domenowego** w obrębie jednego wycinka – łatwiejsze zrozumienie, refaktoryzacja i testowanie konkretnych przypadków użycia.
- **Mniejsza potrzeba globalnych kontraktów** – każdy slice ma własne modele/DTO/handlery.
- **Lepsza modularność** – łatwiej dodawać lub usuwać funkcjonalności bez naruszania reszty systemu.
- **Naturalna skalowalność zespołu** – praca równoległa na niezależnych wycinkach.

### Wady
- **Potencjalna duplikacja** modeli/DTO między wycinkami, zanim zostaną wyniesione do wspólnych miejsc.
- **Wymaga dyscypliny** w decydowaniu, co i kiedy wynosić do `Shared/` lub infrastruktury, aby nie wpaść z powrotem w silne warstwowanie.
- **Trudniejsze na początku** dla zespołów przyzwyczajonych do klasycznego podziału na warstwy (Controller/Service/Repository).

### Ewolucja w stronę modularnego monolitu
- Wraz ze wzrostem niezależności modułów VSA może naturalnie ewoluować w **modularny monolit**.
- Co to zwykle oznacza:
  - Wyraźne granice modułów i ich kontraktów (komendy, zapytania, endpointy).
  - Większą niezależność danych: np. osobne schematy lub osobne `DbContext` per moduł; ograniczanie bezpośrednich kluczy obcych między modułami; współpraca przez kontrakty zamiast bezpośrednich zależności.
  - Niezależniejsze migracje i cykl rozwoju per moduł (w miarę możliwości w monolicie).
- Plusy: lepsza izolacja, łatwiejsza ewentualna ekstrakcja modułu do mikroserwisu w przyszłości.
- Minusy: większa złożoność integracji danych i transakcji między modułami.

### CQRS / CQS
- Projekt stosuje **lekkie CQS/CQRS**: rozdział komend i zapytań na poziomie modeli, handlerów i endpointów (np. `Features/Auth/*Command`, `Features/Comments/Queries/...`, `GetCommentsHandler`, `RegisterUserCommand`).
- Domyślnie używany jest wspólny model i magazyn danych dla odczytu i zapisu (brak osobnych read/write store), więc nie jest to „twarde” CQRS.
- Aby przejść bliżej pełnego CQRS: wprowadzić dedykowane modele odczytu (projekcje), ewentualnie osobny magazyn odczytowy oraz asynchroniczną propagację zmian (np. kolejki/zdarzenia).

---

## Description (EN)

This project demonstrates the VSA (Vertical Slice Architecture) approach in .NET. The goal is to organize code around business features/use-cases rather than technical layers.

### VSA principles
- **Colocation of related code**: Keep all code for a single feature/use-case together. Instead of global layers (Controllers/Services/Repositories), the app is split into **modules** with **endpoints** inside each module.
- **Modules first**: The application is divided into modules (e.g., `Auth`, `Comments`, `Groups`) under `VsaSample/Features/`. Each module contains its own commands, queries, handlers, and endpoints.
- **Extract reusable pieces upward**: When some parts become widely reused, progressively lift them into shared areas like `VsaSample/Shared/` (common `Endpoints`, `Middlewares`, `Responses`) or infrastructure (`VsaSample/Infrastructure/`).

### Project overview
- **Technology**: .NET (minimal APIs / Web API).
- **Structure**: Feature logic in `VsaSample/Features/` (e.g., `Auth`, `Comments`, `Groups`), shared pieces in `VsaSample/Shared/`, data and infrastructure under `VsaSample/Infrastructure/` (e.g., `Data`, `Entities`).
- **Sample features**: user authentication/registration (`Auth`), comments (`Comments`), user groups (`Groups`).

### Repository/project structure
```
sample-vsa-net/
  VsaSample/
    Features/
      Auth/
      Comments/
      Groups/
    Infrastructure/
      Data/
        Context/ (e.g., AppDbContext)
        Entities/
    Shared/
      Endpoints/
      Middlewares/
      Responses/
    Program.cs
```

### Database
- By default the app uses **EF Core InMemory Database** (`UseInMemoryDatabase("TestDb")`).
- No connection strings are required – you can run the project without extra configuration.
- Note: the InMemory provider is not relational (it does not enforce relational constraints). If you need relational semantics in-memory, consider **SQLite In-Memory**.

### Pros
- **Tight domain focus per slice** – easier to understand, refactor, and test individual use-cases.
- **Less need for global contracts** – each slice owns its models/DTOs/handlers.
- **Improved modularity** – features can be added or removed with minimal impact on the rest of the system.
- **Team scalability** – multiple slices can be developed in parallel.

### Cons
- **Possible duplication** of models/DTOs across slices until they are extracted into shared areas.
- **Requires discipline** to decide what and when to lift into `Shared/` or infrastructure to avoid reverting to strict layering.
- **Learning curve** for teams used to traditional layered architecture (Controller/Service/Repository).

### Evolution towards a modular monolith
- As modules become more independent, VSA can naturally evolve into a **modular monolith**.
- This typically implies:
  - Clear module boundaries and contracts (commands, queries, endpoints).
  - Higher data independence: e.g., separate schemas or dedicated `DbContext` per module; minimizing direct cross-module foreign keys; collaborating via contracts instead of direct references.
  - More independent migrations and development cadence per module (as far as feasible within a monolith).
- Pros: stronger isolation, easier future extraction of a module into a microservice.
- Cons: increased complexity of data integration and cross-module transactions.

### CQRS / CQS
- The project follows a **lightweight CQS/CQRS** approach: commands and queries are separated at the level of models, handlers, and endpoints (e.g., `Features/Auth/*Command`, `Features/Comments/Queries/...`, `GetCommentsHandler`, `RegisterUserCommand`).
- By default, a single model/store is used for both reads and writes (no separate read/write stores), so this is not strict CQRS.
- To move towards stricter CQRS: introduce dedicated read models (projections), potentially a separate read store, and asynchronous propagation (e.g., queues/events).
