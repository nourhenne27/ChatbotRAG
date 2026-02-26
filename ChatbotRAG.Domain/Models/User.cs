using System;
using System.Collections.Generic;
/*🔹 System

Permet d’utiliser :

DateTime

🔹 System.Collections.Generic

Permet d’utiliser :

ICollection<T>

List<T>*/
namespace Domain.Models
{
    /// <summary>
    /// Représente un utilisateur du système avec ses profils et permissions
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        /*Clé primaire

         Identifiant unique

         Généré automatiquement par SQL Server (Identity)*/

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        /*Email unique

        Utilisé pour authentification*/

        public string PasswordHash { get; set; } = string.Empty;
        /*On ne stocke jamais le mot de passe brut

             On stocke un hash sécurisé

            Exemple :

            BCrypt

            PBKDF2*/

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        /*Permet :

Désactiver un utilisateur sans le supprimer

Soft disable

En entreprise c’est très utilisé.*/

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        /*Date de création

Stockée en UTC (bonne pratique mondiale)

Pourquoi UTC ?
Pour éviter les problèmes de fuseaux horaires.*/

        public DateTime? LastLoginAt { get; set; }
        /*Le ? signifie :

➡ Nullable

Donc :

Peut être null

Null = l’utilisateur ne s’est jamais connecté*/

        // Navigation properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

        public virtual ICollection<Document> UploadedDocuments { get; set; } = new List<Document>();
    }
}
