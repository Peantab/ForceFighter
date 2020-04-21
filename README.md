# Force Fighter

Projekt na przedmiot *Systemy Mobilne*

**Autorzy:** Paweł Taborowski, Agnieszka Warchoł, Wojciech Żdżarski

*Unity 2019.3.6f1*

## Uwagi do prototypu:

 * Sterowanie myszką (lub palcem na ekranie dotykowym) - dodajesz siły obiektom z obszaru pod kursorem
 * Hipotetycznie myszkę powinno dać się podmienić na dowolny inny kontroler, wyświetlanie kursora też jest opcjonalne
 * Są cztery obszary - lewy górny, lewy dolny, prawy górny, prawy dolny
 * Gdy obiekt stworzony jako "górny" dotknie podłogi, staje się "dolny"
 * Przepuszczenie kuli - -20 HP. Gdy licznik HP spadnie do zera, licznik punktów (sekund od początku gry) zostaje zamrożony (ale można grać dalej bez punktów)
 * W tej wersji poziom trudności jest stały w czasie (wylatuje jedna kula na sekundę) - docelowo myślę, by tym manipulować
 * Aby dodać nowy rzucany obiekt, należy ustawić mu tag na `ThrownObject` i załączyć skrypt `ItemProperties`
 * Wizualnie jest to wstępny prototyp, dźwiękowo... nie ma dźwięku