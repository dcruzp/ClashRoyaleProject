# Clash Royale Web Aplication 

### Estructura del proyecto por carpetas 

- **DBModels**

    Aqui estan todas las clases que me representan la base Datos 
    
    ```
        DBModels
            |- Batalla.cs 
            |- Cartum.cs 
            |- Clan.cs 
            |- ClashroyaleContext.cs 
            |- Desafio.cs
            |- Dispone.cs
            |- Donar.cs
            |- Estructura.cs
            |- GuerradeClane.cs
            |- Hechizo.cs
            |- Jugador.cs
            |- Lucha.cs
            |- Miembro.cs
            |- Participa.cs
            |- ParticipaEn.cs
            |- Pertenece.cs
            |- Tropa.cs
    ```

     - La clase  *Batalla.cs* me representa la batalla
        entre dos jugadores 

    - La clase  *Cartum.cs* me representa una carta 

    - La clase *Clan.cs* me representa una Clan 

    - La clase *Desafio.cs* representa un desafio 

    - La clase *Dispone.cs* es una representacion de un
    tabla relacional entre un jugador y una carta. para saber 
    las cartas de las que dispone un jugador 

    - La clase *Donar.cs* me representa una donacion hecha 
    por una jugador miembro de un clan  de una carta determinada

    - La clase *Estructura.cs* me representa una estructura, 
     que es una especializacion de Carta 

    - La clase *GuerradeClane.cs* me representa una guerra
    que puede ocurrir entre varios clanes

    - La clase *Hechizo.cs*  representa una especializacion de Carta 

    - La clase *Jugador.cs* me representa un jugador

    - La clase *Lucha.cs* me representa una lucha que 
    se puede dar entre dos jugadores

    - La clase  *Miembro.cs* me representa un los miembros
    de los clanes , es decir la relacion de los jugadores con los clanes

    - La clase  *Participa.cs* es para representar la participacion 
    de un jugador en un desafio

    - La clase  *ParticipaEn.cs* es para representar la participacion 
    de un clan en una guerra 

    - La clase *Pertenece.cs* es para representar los jugadores que pertenecen 
    a un clan. 

    - La Clase *Tropa.cs* es una espacializacion de carta  
 

 - **Data**

    En esta carpeta se encuentras las clases e interfaces que 
    se usan para hacer inserciones , actualizaciones y barrado de 
    la base de datso
    
 - **Models**
    
    Esto me representa los modelos que se usan para 
    representar los objetos que son enviados al 
    frontend 

     
    ```
        Models
            |- CartaModels.cs 
            |- ClanModels.cs 
            |- DesafioModels.cs 
            |- GuerraDeClanesModels.cs 
            |- JugadorModels.cs            
    ```

  - **Data**

    En esta carpeta estan todas la interfaces e implementaciones 
    de los datos y las query para manejar la base de datos 
     ```
        Data
            |- ICartaRepository.cs 
            |- IClanRepository.cs 
            |- IDesafioRepository.cs 
            |- IGuerraDeClanesRepository.cs 
            |- IJugadorRepository.cs
            |- ClashRoayaleProfile.cs            
    ```

 ## Diagrama de la Base de Datos

   ![imag](img/DBDiagram.png)